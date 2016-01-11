
require("ForeachTest")
--引用区域
tbl = {"alpha","beta","gamma"}
local tbl2 = {1,2}


--数据区域
function test(...)
  print(...)
  local args,go,fuck,fuc = ...;
end

local function myRunning()
  print("myRunning()")
end

local function main()
--  print("my is main function")
--  test("my is main function","hello","fuck",myRunning)
    foreachITest()
    print("======================")
    foreachTest()
    print("======================")
--    assert(tb2,"========tb2 is null=========")
--    table.setn(tb2,10)
--    for i =1,10 do
--      tb2[i] = i
--    end
--    print(table.getn(tb2))
    
end
main()
