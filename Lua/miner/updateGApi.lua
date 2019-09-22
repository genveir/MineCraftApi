local baseUri = "http://84.104.212.172:4800/api/bot/"

local result = http.get(baseUri .. "GApiUpdate")

if not result then
	print("Cannot reach server")
	return
end

local responseCode = result.getResponseCode()

if (responseCode == 200) then
	local parsedResult = result.readAll()
	local handle = fs.open("GApi", "w")
	handle.write(parsedResult)
	handle.close()
	print("GApi Updated")
else
	print("Error updating GApi")
end