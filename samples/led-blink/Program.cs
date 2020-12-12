using System;
using System.Device.Gpio;
using System.Threading;

namespace BlinkDocker
{
    class Program
    {
        static void Main(string[] args)
        {
            var pin = 18;
            var lightTime = 1000;
            var dimTime = 500;

            if (args.Length == 1)
            {
                Console.WriteLine($"Lighttime parameter given: {args[0]}");
                lightTime = int.Parse(args[0]);
            }

            Console.WriteLine($"Let's blink an LED!");
            using GpioController controller = new();
            controller.OpenPin(pin, PinMode.Output);
            Console.WriteLine($"GPIO pin enabled for use: {pin}");

            Console.CancelKeyPress += (s, e) =>
            {
                // turn off all pins when the program is terminated, with CTRL-C
                Console.WriteLine($"Dim pin {pin}");
                controller.Write(pin, PinValue.Low);
                Console.WriteLine("All off, program ended by user.");
            };

            // turn LED on and off
            try
            {
                do
                {
                    Console.WriteLine($"Light for {lightTime}ms");
                    controller.Write(pin, PinValue.High);
                    Thread.Sleep(lightTime);

                    Console.WriteLine($"Dim for {dimTime}ms");
                    controller.Write(pin, PinValue.Low);
                    Thread.Sleep(dimTime);

                    // Decrease by 10%
                    lightTime = lightTime - ((lightTime / 100) * 10);
                    dimTime = dimTime - ((dimTime / 100) * 10);

                } while (lightTime > 0 && dimTime > 0);
            }
            catch (Exception e)
            {
                controller.Write(pin, PinValue.Low);
                Console.WriteLine(e);
            }

            Console.WriteLine($"The show is over...");

        }
    }
}
