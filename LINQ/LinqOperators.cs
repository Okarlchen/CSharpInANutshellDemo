using LINQ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ
{
    public class LinqOperators
    {
        private string[] names = { "Tom", "Dick", "Harry", "Mary", "Jay" };

        public void WhereOperator()
        {
            //Result: { "Harry", "Mary", "Jay" }
            IEnumerable<string> query = names.Where(name => name.EndsWith("y"));

            //查询表达式
            IEnumerable<string> query1 = from name in names
                                         where name.EndsWith("y")
                                         select name;

            //配合let、order by 和join子句，可以多次使用where
            IEnumerable<string> query3 = from name in names
                                         where name.Length > 3
                                         let u = name.ToUpper()
                                         where u.EndsWith("Y")
                                         select u;

            //索引筛选。 第二个可选类型参数用于指定输入序列中每个元素的位置，并可以用于元素的筛选。在EF或者LINQ to SQL中使用索引筛选会抛出异常
            //下面例子中，查询会跳过偶数位置的元素。 Result: { "Tom", "Harry", "Jay" }
            IEnumerable<string> query4 = names.Where((n, i) => i % 2 == 0);
        }

        public void TakeAndSkipOperator()
        {
            Console.WriteLine("=====Take:=====");

            //Result: { "Tom", "Dick" }
            IEnumerable<string> query = names.Take(2);
            foreach (string n in query) Console.WriteLine(n);

            Console.WriteLine("=====Skip:=====");
            //Result: { "Harry", "Mary", "Jay" }
            IEnumerable<string> query1 = names.Skip(2);
            foreach (string n in query1) Console.WriteLine(n);

            Console.WriteLine("=====分页查询:=====");
            //组合使用，可用于分页查询。查询第3个到第4个结果 Result: { "Harry", "Mary" }
            IEnumerable<string> query2 = names.Skip(2).Take(2);
            foreach (string n in query2) Console.WriteLine(n);

        }

        public void TakeWhileAndSkipWhileOperator()
        {
            int[] numbers = { 3, 5, 2, 234, 4, 1 };

            Console.WriteLine("=====TakeWhile:=====");

            //Result: { 3,5,2 }
            IEnumerable<int> query = numbers.TakeWhile(n => n < 100);
            foreach (int n in query) Console.WriteLine(n);

            Console.WriteLine("=====SkipWhile:=====");
            //Result: { 234,4,1 }
            IEnumerable<int> query1 = numbers.SkipWhile(n => n < 100);
            foreach (int n in query1) Console.WriteLine(n);

        }

        public void DistinctOperator()
        {
            Console.WriteLine("=====Distinct:=====");

            //Result: HelloWrd
            char[] distinctLetters = "HelloWorld".Distinct().ToArray();
            string s = new string(distinctLetters);
            Console.WriteLine(s);
        }

        public void SelectOperator()
        {
            Console.WriteLine("=====Select:=====");

            List<Person> persons = new List<Person> {  
                new Person { Name = "Tom", Age = 22, Sex = "Men" }, 
                new Person { Name = "Dick", Age = 24, Sex = "Men" }, 
                new Person { Name = "Harry", Age = 25, Sex = "Woman" },
                new Person { Name = "Mary", Age = 26, Sex = "Woman" },
                new Person { Name = "Jay", Age = 27, Sex = "Men" }
            };

            Console.WriteLine("==映射Name:==");
            //映射Name
            IEnumerable<string> query = persons.Select(p => p.Name);
            //与上面等价
            IEnumerable<string> query1 = from p in persons
                                        select p.Name;
            foreach (string name in query) Console.WriteLine(name);

            //有时查询语法的映射不会执行任何转换，仅仅是为了满足查询必须以select结尾的语法要求。
            IEnumerable<Person> query3 = from p in persons
                                         where p.Sex == "Men"
                                         select p;

            Console.WriteLine("==索引映射:==");
            //索引映射
            IEnumerable<string> query4 = persons.Select((p, i) => i + ":" + p.Name);
            foreach (string s in query4) Console.WriteLine(s);

        }

        public void SelectManyOperator()
        {
            Console.WriteLine("=====SelectMany:=====");

            var fullNames = new[] { "Anne Williams", "John Fred Smith", "Sue Green" }.AsQueryable();

            //展开子序列，将嵌套的集合平面化
            IEnumerable<string> query1 = fullNames.SelectMany(name => name.Split());
            
            //和上面是等效的
            IEnumerable<string> query2 = from fullName in fullNames
                                         from name in fullName.Split()
                                         select name;
            foreach (string s in query1) Console.WriteLine(s);
            



        }
    }
}
