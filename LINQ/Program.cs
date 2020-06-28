using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            //数组类型继承了IEnumerable，因此可以使用LINQ查询
            string[] names = { "Tom", "Dick", "Harry","Mary","Jay" };

            IEnumerable<string> filteredNames1 = System.Linq.Enumerable.Where(names, n => n.Length >= 4);

            //使用静态扩展方法
            IEnumerable<string> filteredNames2 = names.Where(n => n.Length >= 4);

            //使用var隐式声明filteredNames3的类型，进一步精简代码
            var filteredNames3 = names.Where(n => n.Length >= 4);

            //查询表达式
            var filteredNames4 = from n in names
                                 where n.Length >= 4
                                 select n;

            //查询运算符链
            IEnumerable<string> query = names
                .Where(n => n.Contains("a"))
                .OrderBy(n => n.Length)
                .Select(n => n.ToUpper());

            foreach (string n in filteredNames2)
            {
                Console.WriteLine(n);
            }

            LinqOperators linqOperators = new LinqOperators();
            linqOperators.TakeAndSkipOperator();
            linqOperators.TakeWhileAndSkipWhileOperator();
            linqOperators.DistinctOperator();
            linqOperators.SelectOperator();
            linqOperators.SelectManyOperator();
            Console.ReadLine();
        }
    }
}
