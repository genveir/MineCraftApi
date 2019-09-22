os.loadAPI("GApi")

local test = GApi.test()

if test then
	GApi.logon()
	GApi.update()
	shell.run("bot")
else
	print("server offline")
end