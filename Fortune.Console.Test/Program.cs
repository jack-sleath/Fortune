using System;
using System.Device.Gpio;
using System.Threading;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Raspberry Pi GPIO LED Flash Test");

        // Choose the GPIO pin (for example, pin 18)
        int ledPin = 18;

        try
        {
            // Create a new GpioController object
            using (GpioController controller = new GpioController(PinNumberingScheme.Board))
            {
                // Open the GPIO pin for output
                controller.OpenPin(ledPin, PinMode.Output);

                // Flash the LED 5 times
                for (int i = 0; i < 5; i++)
                {
                    // Turn the LED on
                    controller.Write(ledPin, PinValue.High);
                    Console.WriteLine("LED ON");
                    Thread.Sleep(500); // Wait for 500 milliseconds

                    // Turn the LED off
                    controller.Write(ledPin, PinValue.Low);
                    Console.WriteLine("LED OFF");
                    Thread.Sleep(500); // Wait for 500 milliseconds
                }

                // Clean up: Close the pin
                controller.ClosePin(ledPin);
            }
        }
        catch (PlatformNotSupportedException ex)
        {
            Console.WriteLine("GPIO is not supported on this platform.");
            Console.WriteLine($"Exception: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while accessing the GPIO.");
            Console.WriteLine($"Exception: {ex.Message}");


        }
    }
}