local function selectStone()
	for n=1,16,1 down
		turtle.select(n)
		local details = turtle.getItemDetail(n)
		if (details.name == "minecraft:cobblestone" and nCount > 24) then
			return
		end
	end
end

shell.run("refuel")
turtle.down()
turtle.turnLeft()
turtle.turnLeft()

selectStone()

alternate = 0
for rows=1,5,1 do
	placeAndWalk(5)
	if row < 5 then
		if alternate = 0 then turtle.turnRight() end
		else turtle.turnLeft() end

		turtle.forward()

		if alternate = 0 then turtle.turnRight() end
		else turtle.turnLeft() end
	end
end
