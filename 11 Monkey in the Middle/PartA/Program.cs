using System.Diagnostics;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace AoC
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var sr = new StreamReader(@"..\..\input.txt");
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                if (String.IsNullOrEmpty(line)) continue;

                var m = Regex.Match(line, @"^Monkey (\d+):$");
                Trace.Assert(m.Success);

                int number = int.Parse(m.Groups[1].Value);
                Trace.Assert(number == Monkey.monkeys.Count);

                line = sr.ReadLine();
                m = Regex.Match(line, @"^  Starting items: ((\d+)[ ,]*)*$");
                var items = new List<long>();
                foreach (var item in m.Groups[2].Captures)
                {
                    items.Add(long.Parse(item.ToString()));
                }

                line = sr.ReadLine();
                m = Regex.Match(line, @"^  Operation: new = (.+) (.) (.+)$");
                Trace.Assert(m.Success);
                Monkey.Operation operation = Monkey.Operation.Unknown;
                int operand = 0;
                if (m.Groups[2].Value == "+")
                {
                    operation = Monkey.Operation.Add;
                    operand = int.Parse(m.Groups[3].Value);
                }
                else if (m.Groups[2].Value == "*")
                {
                    if (m.Groups[3].Value == "old")
                    {
                        operation = Monkey.Operation.Square;
                    }
                    else
                    {
                        operation = Monkey.Operation.Multiply;
                        operand = int.Parse(m.Groups[3].Value);
                    }
                }
                Trace.Assert(operation != Monkey.Operation.Unknown);

                line = sr.ReadLine();
                m = Regex.Match(line, @"^  Test: divisible by (.+)$");
                Trace.Assert(m.Success);
                int divtest = int.Parse(m.Groups[1].Value);

                line = sr.ReadLine();
                m = Regex.Match(line, @"^    If true: throw to monkey (.+)$");
                Trace.Assert(m.Success);
                int targettrue = int.Parse(m.Groups[1].Value);

                line = sr.ReadLine();
                m = Regex.Match(line, @"^    If false: throw to monkey (.+)$");
                Trace.Assert(m.Success);
                int targetfalse = int.Parse(m.Groups[1].Value);

                Monkey.monkeys.Add(new Monkey(number, items, operation, operand, divtest, targettrue, targetfalse));
            }

            for (int round = 0; round < 20; round++)
            {
                Console.WriteLine($"### Round {round + 1} ###");
                foreach (var monkey in Monkey.monkeys)
                {
                    monkey.Inspect();
                }
            }

            int first = 0;
            int second = 0;
            foreach (var monkey in Monkey.monkeys)
            {
                if (monkey.inspections > first)
                {
                    if (first > second) second = first;
                    first = monkey.inspections;
                }
                else if (monkey.inspections > second)
                {
                    second = monkey.inspections;
                }
                Console.WriteLine($"Monkey {monkey.number} inspected items {monkey.inspections} times.");
            }
            Console.WriteLine($"First: {first} Second: {second} Product: {first * second}");
        }
    }

    class Monkey
    {
        static public readonly List<Monkey> monkeys = new List<Monkey>();

        public enum Operation { Unknown, Add, Multiply, Square };

        public int number { get; private set; }
        public List<long> items;
        public Operation operation;
        public int operand;
        public int divtest;
        public int targettrue;
        public int targetfalse;
        public int inspections { get; private set; } = 0;

        public Monkey(int number, List<long> items, Operation operation, int operand, int divtest, int targettrue, int targetfalse)
        {
            this.number = number;
            this.items = items;
            this.operation = operation;
            this.operand = operand;
            this.divtest = divtest;
            this.targettrue = targettrue;
            this.targetfalse = targetfalse;
        }

        public void Inspect()
        {
            Console.WriteLine($"Monkey {number} has {items.Count} items");
            for (int i = 0; i < items.Count; i++)
            {
                Console.WriteLine($"  Monkey inspects an item with a worry level of {items[i]}");
                inspections++;
                if (operation == Operation.Add)
                {
                    items[i] += operand;
                    Console.WriteLine($"    Worry level increases by {operand} to {items[i]}");
                }
                else if (operation == Operation.Multiply)
                {
                    items[i] *= operand;
                    Console.WriteLine($"    Worry level is multiplied by {operand} to {items[i]}");
                }
                else if (operation == Operation.Square)
                {
                    items[i] *= items[i];
                    Console.WriteLine($"    Worry level is multiplied by itself to {items[i]}");
                }
                items[i] /= 3;
                Console.WriteLine($"    Monkey gets bored with item. Worry level is divided by 3 to {items[i]}");
                int target;
                if (items[i] % divtest == 0)
                {
                    Console.WriteLine($"    Current worry level is divisible by {divtest}.");
                    target = targettrue;
                }
                else
                {
                    Console.WriteLine($"    Current worry level is not divisible by {divtest}.");
                    target = targetfalse;
                }
                Console.WriteLine($"    Item with worry level {items[i]} is thrown to monkey {target}.");
                monkeys[target].items.Add(items[i]);
            }
            items.Clear();
        }
    }
}