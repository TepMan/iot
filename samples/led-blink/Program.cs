// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Device.Gpio;
using System.Threading;

var pin = 18;
var lightTime = 1000;
var dimTime = 500;

Console.WriteLine($"Let's blink an LED!");
using GpioController controller = new();
controller.OpenPin(pin, PinMode.Output);
Console.WriteLine($"GPIO pin enabled for use: {pin}");

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

        lightTime = lightTime - 10;
        dimTime = dimTime - 10;
    
    } while (lightTime > 0 && dimTime > 0);
}
catch (Exception e)
{
    controller.Write(pin, PinValue.Low);
    Console.WriteLine(e);
}

Console.WriteLine($"The show is over...");
