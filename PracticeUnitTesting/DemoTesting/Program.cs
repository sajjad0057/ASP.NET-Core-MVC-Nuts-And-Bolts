
using DemoLib;

string line = Console.ReadLine();

int[] nums = line.Split(' ').Select(x => int.Parse(x)).ToArray();

Calculator calculator = new Calculator();

int res = calculator.Sum(nums[0], nums[1]);

Console.WriteLine(res);
