local baseUri = "http://84.104.212.172:4800/api/bot/"

function getApiId()
	local fileExists = fs.exists("ApiId")

	if fileExists then	
		local file = fs.open("ApiId", "r")
		local content = file.readAll()
		file.close()

		if content then
			return true, content
		end
	end

	return false, ""
end

function setApiId(apiId)
	local file = fs.open("ApiId", "w")
	file.write(apiId)
	file.close()

	os.setComputerLabel("Bot " .. apiId)
end

function split (inputstr)
    local t={}
	local num = 0
    for str in string.gmatch(inputstr, "([^;]+)") do
		num = num + 1
		table.insert(t, str)
    end
    return num, t
end

function test()
	local configOK = http.checkURL(baseUri)
	if not configOK then
		return false
	end

	local result = http.get(baseUri)

	if not result then return false end

	if (result.getResponseCode() == 200) then
		return true
	else
		return false
	end
end

function logon()
	local success, apiId = getApiId()

	local uri = ""
	if (success) then 
		uri = baseUri .. apiId .. "/logon"
	else
		uri = baseUri .. "logon"
	end

	local result = http.post(uri, "")
	local responseCode = result.getResponseCode()

	if (responseCode == 201) then
		setApiId(result.readAll())
		return true
	elseif (responseCode == 200) then
		setApiId(result.readAll())
		return true
	else
		return false
	end
end

local function parseReport(reportResult)
	return reportResult
end

function report(data)
	local hasId, id = getApiId()

	local result = http.post(baseUri .. id .. "/report", data)
	local responseCode = result.getResponseCode()

	if (responseCode == 200) then
		return true, parseReport(result.readAll())
	else
		return false, "server error"
	end
end

local function updateFiles(updateResult)
	local num, toUpdate = split(updateResult)

	for n=1,num,2 do
		local fileName = toUpdate[n]
		local fileContent = toUpdate[n+1]

		local handle = fs.open(fileName, "w")
		handle.write(fileContent)
		handle.close()
	end
end

function update()
	local hasId, id = getApiId()

	local result = http.get(baseUri .. id .. "/update")
	local responseCode = result.getResponseCode()

	if (responseCode == 200) then
		local parsedResult = result.readAll()
		updateFiles(parsedResult)
		return true
	else
		return false
	end
end