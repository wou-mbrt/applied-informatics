using System;
using System.Collections.Generic;

namespace Лаба7
{
    class Program
    {
        static void Main(string[] args)
        {
            NormalMarkovAlgorithms normalMarkov = new NormalMarkovAlgorithms();
            normalMarkov.AddRule(new Rule() { leftSide = "*a", rightSide = "aA*" });
            normalMarkov.AddRule(new Rule() { leftSide = "*b", rightSide = "bB*" });
            normalMarkov.AddRule(new Rule() { leftSide = "*", rightSide = "#" });
            normalMarkov.AddRule(new Rule() { leftSide = "Aa", rightSide = "aA" });
            normalMarkov.AddRule(new Rule() { leftSide = "Ab", rightSide = "bA" });
            normalMarkov.AddRule(new Rule() { leftSide = "Ba", rightSide = "aB" });
            normalMarkov.AddRule(new Rule() { leftSide = "Bb", rightSide = "bB" });
            normalMarkov.AddRule(new Rule() { leftSide = "A#", rightSide = "#a" });
            normalMarkov.AddRule(new Rule() { leftSide = "B#", rightSide = "#b" });
            normalMarkov.AddRule(new Rule() { leftSide = "#", rightSide = "" , isTerminal = true});
            normalMarkov.AddRule(new Rule() { leftSide = "", rightSide = "*" });
            Console.WriteLine(normalMarkov.Execute("AabbAadadbabababaaa#aA*aa"));
        }
    }

    public struct Rule
    {
        public string leftSide;
        public string rightSide;
        public bool isTerminal;

    }

    public class NormalMarkovAlgorithms
    {
        public List<Rule> rules { set; get; }

        public void SetRules(List<Rule> rules)
        {
            this.rules = rules;
        }

        public void AddRule(Rule rule)
        {
            if(rules == null)
            {
                rules = new List<Rule>();
            }
            rules.Add(rule);
        }

        public string Execute(string input)
        {
            foreach (var rule in rules)
            {
                if (rule.leftSide == "")
                    return rule.isTerminal ? AddAtStart(input, rule.rightSide) : Execute(AddAtStart(input, rule.rightSide));
                if (rule.rightSide == "")
                    return rule.isTerminal ? RemoveFirst(input, rule.leftSide) : Execute(RemoveFirst(input, rule.leftSide));
                if (input.Contains(rule.leftSide))
                    return rule.isTerminal ? ReplaceFirst(input, rule.leftSide,rule.rightSide) : Execute(ReplaceFirst(input, rule.leftSide, rule.rightSide));
            }

            return input;
        }

        private string ReplaceFirst(string input, string removeString, string addedString)
        {
            int pos = input.IndexOf(removeString);
            if (pos < 0)
            {
                return input;
            }
            return input.Substring(0, pos) + addedString + input.Substring(pos + removeString.Length);
        }

        private string RemoveFirst(string input, string removeString)
        {
            int index = input.IndexOf(removeString);
            string cleanPath = (index < 0)
                ? input
                : input.Remove(index, removeString.Length);
            return cleanPath;
        }

        private string AddAtStart(string input, string addedString)
        {
            return addedString + input;
        }
    }
}
