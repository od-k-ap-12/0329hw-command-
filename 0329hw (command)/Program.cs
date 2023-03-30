using _0329hw__command_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace _0329hw__command_
{
    public class TV
    {

        public void On()
        {
            Console.WriteLine("TV is on!");
        }
        public void Off()
        {
            Console.WriteLine("TV is off!");
        }
    };

    public class Microwave
    {
        public void StartCooking(int time)
        {
            Console.WriteLine("Warming up food...");
            Thread.Sleep(time);
        }
        public void StopCooking()
        {
            Console.WriteLine("The food is warmed up!");
        }
    };

    public abstract class ICommand
    {
        public abstract void Execute();
        public abstract void Undo();
    }

    public class TVOnCommand : ICommand
    {
        TV tv;
        public TVOnCommand(TV tvSet)

        {
            tv = tvSet;
        }
        public override void Execute()
        {
            tv.On();
        }
        public override void Undo()
        {
            tv.Off();
        }
    };

    public class MicrowaveCommand : ICommand
    {
        Microwave microwave;
        int time;
        public MicrowaveCommand(Microwave m, int t)

        {
            microwave = m;
            time = t;
        }
        public override void Execute()
        {
            microwave.StartCooking(time);
            microwave.StopCooking();
        }
        public override void Undo()
        {
            microwave.StopCooking();
        }
    };

    class Controller
    {
        ICommand command;
        public Controller() { }
        public void SetCommand(ICommand com)
        {
            command = com;
        }
        public void PressButton()
        {
            if (command != null)
                command.Execute();
        }
        public void PressUndo()
        {
            if (command != null)
                command.Undo();
        }
    };

    internal class Program
    {
        public static void Invoker(ICommand command, bool Undo)
        {
            Controller controller = new Controller();
            controller.SetCommand(command);
            controller.PressButton();
            if (Undo)
                controller.PressUndo();
        }

        static void Main(string[] args)
        {
            TV tv = new TV();
            ICommand command = new TVOnCommand(tv);
            Invoker(command, true);

            Microwave microwave = new Microwave();
            command = new MicrowaveCommand(microwave, 5000);
            Invoker(command, false);
        }
    }
}
