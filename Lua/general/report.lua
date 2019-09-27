print("hallo, dit is bot {{BOTNAME}}")

os.loadAPI("GApi")

local compiled = "data="

print("compiling report")
local function report(message)
	print(message)
	compiled = compiled .. textutils.urlEncode(message) .. "<*>"
end

turtle.select(16)
local details = turtle.getItemDetail(16)
if (details) then
	print("slot 16 is not empty, can't check tool")
else
	turtle.equipLeft()
	local details = turtle.getItemDetail(16)
	if (details) then
		report("tool left:" .. details.name)
		turtle.equipLeft()
	end

	turtle.equipRight()
	local details = turtle.getItemDetail(16)
	if (details) then
		report("tool right:" .. details.name)
		turtle.equipRight()
	end
end

local fuel = turtle.getFuelLevel()
report("fuel:" .. fuel)

report("label:" .. os.getComputerLabel())

GApi.report(compiled)