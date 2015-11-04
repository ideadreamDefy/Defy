using System;
namespace Model{
	public class People{
		public int age() {get;set;}
		public string name{get;set;}
		public People(int age,string name){
			this.age = age;
			this.name = name;
		}
	}
}
