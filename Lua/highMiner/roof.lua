local function selectStone()
	for n=1,16,1 do
		turtle.select(n)
		local details = turtle.getItemDetail(n)
		if (details) then
			if (details.name == "minecraft:cobblestone" and details.count > 24) then
				return
			end
		end
	end
end

local function placeAndWalk(num)
	for n=1,num,1 do
		turtle.placeDown()
		turtle.forward()
	end
	turtle.placeDown()
end

shell.run("refuel")

selectStone()

local alternate = 0
for row=1,5,1 do
	placeAndWalk(4)
	if (row < 5) then
		if (alternate == 0) then 
			turtle.turnRight() 
		else 
			turtle.turnLeft() 
		end

		turtle.forward()

		if (alternate == 0) then 
			turtle.turnRight()
		else 
			turtle.turnLeft() 
		end

		alternate = 1 - alternate
	end
end

turtle.turnLeft()
for n=1,4,1 do
	turtle.forward()
end
turtle.turnRight()
turtle.dig()
turtle.forward()