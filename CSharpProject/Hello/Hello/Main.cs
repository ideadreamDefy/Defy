using System;
using Tool;
using Model;
namespace Hello
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			MainHello hello = new MainHello();
			hello.printTable();
			Console.WriteLine ("Hello World!");

			List<People> people = new List<People>();
			People p1 = new People(21,"guojing");
			People p2 = new People(21,"wujunmin");
			People p3 = new People(21,"muqing";
			People p4 = new People(21,"lipan");
			people.add(p1);
			people.add(p2);
			people.add(p3);
			people.add(p4);
		}

	}
}
