using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace digital
{
    class Program
    {
        abstract class Component
        {
            public List<Node> inputs = new List<Node>();
            public abstract bool GetState();
            public void Reset()
            {
                foreach (var input in inputs)
                {
                    input.Reset();
                }
            }
        }
        class And : Component
        {
            public override bool GetState()
            {
                return inputs[0].Value && inputs[1].Value;
            }
        }
        class Or : Component
        {
            public override bool GetState()
            {
                return inputs[0].Value || inputs[1].Value;
            }
        }
        class Xor : Component
        {
            public override bool GetState()
            {
                return inputs[0].Value != inputs[1].Value;
            }
        }
        class Invert : Component
        {
            public override bool GetState()
            {
                return !inputs[0].Value;
            }
        }
        class Clock:Node
        {
            public bool CurrentValue = true;
            public int Step;
            private int CurStep = 0;
            public void Tick()
            {
                if ((++CurStep) == Step)
                {
                    CurStep = 0;
                    CurrentValue = !CurrentValue;
                }
            }
            public override void ComputeValue()
            {
                value = CurrentValue;
            }
        }
        class Input:Node
        {
            public bool CurrentValue;
            public override void ComputeValue()
            {
                value = CurrentValue;
            }
        }
        class Node
        {
            protected bool? value = null;
            public bool Value
            {
                get
                {
                    if (value == null)
                        ComputeValue();
                    return value.Value;
                }
                set
                {
                }
            }
            public void Reset()
            {
                value = null;
            }
            public virtual void ComputeValue()
            {
                value = input.GetState();
            }
            public Component input;
        }
        static Dictionary<string, Type> thingamajiggers = new Dictionary<string, Type>()
        {
            { "I", typeof(Invert)},
            {"O", typeof(Or)},
            { "A", typeof(And)},
            {"X", typeof(Xor)}
        };
        static void Main(string[] args)
        {
            //System.IO.TextReader read = new System.IO.StreamReader("input.txt");
            System.IO.TextReader read = Console.In;
            Clock clock = new Clock();
            Node output = new Node();
            Input input = new Input();
            string[] settings = read.ReadLine().Split(' ');
            int numComponents = int.Parse(settings[1]);
            clock.Step = int.Parse(settings[0]) + 1;

            if (int.Parse(settings[0]) == 2)
                clock.Step = 4;

            Dictionary<int,Node> nodes = new Dictionary<int,Node>();
            nodes.Add(0,output);
            nodes.Add(1,clock);
            nodes.Add(2,input);

            string inputStream = read.ReadLine();

            for(int i=0;i<numComponents;i++)
            {
                string[] compSetting = read.ReadLine().Split(' ');
                Component component = thingamajiggers[compSetting[0]].GetConstructor(new Type[]{}).Invoke(new Object[]{}) as Component;
                var compOutput = int.Parse(compSetting.Last());
                if (!nodes.ContainsKey(compOutput))
                    nodes.Add(compOutput, new Node());
                nodes[compOutput].input = component;
                foreach (var compInput in compSetting.Skip(1).Take(compSetting.Length - 2))
                {
                    var compInputInt = int.Parse(compInput);

                    if (!nodes.ContainsKey(compInputInt))
                        nodes.Add(compInputInt, new Node());
                    component.inputs.Add(nodes[compInputInt]);
                }
            }

            string[] clockSpeed = { "101010101010101010", "1100110011001100", "1111000011110000" };


            for (int i = 0; i < 10; i++)
            {
                foreach (KeyValuePair<int,Node> node in nodes.Skip(3))
                {
                    node.Value.Reset();
                }
                input.Reset();
                output.Reset();
                input.CurrentValue = inputStream[i] == '1';
                clock.CurrentValue = clockSpeed[int.Parse(settings[0])][i]=='1';
                Console.Write(output.Value ? 1 : 0);
            }
            //Console.ReadKey();
        }
    }
}
