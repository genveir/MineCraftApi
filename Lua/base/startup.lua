os.loadAPI("GApi")

local test = GApi.test()

if test then
	GApi.logon()
	shell.run("general/report")
	GApi.update()
	shell.run("general/bot")
else
	print("server offline")
end