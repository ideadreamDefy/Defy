local t1 = {2,3,6,language = "lua",version = "5",8,10,12,web = "hello lua"}

--对表里整数表达式进行遍历
function foreachITest()
  table.foreachi(t1,function(i,v)
     print(i,v)
  end)
end

--对表里所有表达式遍历
function foreachTest()
  table.foreach(t1,function(i,v)
     print(i,v)
  end)
end