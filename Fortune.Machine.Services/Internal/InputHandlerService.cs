using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortune.Machine.Services.Internal
{
    public class InputHandlerService : IDisposable
    {
        private readonly GpioController _controller;
        private readonly int _inputPin;
        private readonly GpioService _gpioService;

        public InputHandlerService(GpioService gpioService)
        {
            _inputPin = Settings.ButtonPin;
            _gpioService = gpioService;
            _controller = new GpioController(PinNumberingScheme.Board);

            // Open the input pin for reading input (e.g., a button press)
            _controller.OpenPin(_inputPin, PinMode.InputPullUp);
        }

        public void StartListening()
        {
            Console.WriteLine("Listening for button press...");

            while (true)
            {
                // Wait for a button press (PinValue.Low indicates button press)
                if (_controller.Read(_inputPin) == PinValue.Low)
                {
                    Console.WriteLine("Button pressed!");

                    // Call the LED flash method
                    _gpioService.FlashLed(5, 500);
                }

                // Add a small delay to avoid excessive CPU usage
                Thread.Sleep(100);
            }
        }

        public void Dispose()
        {
            // Clean up and close the input pin
            if (_controller.IsPinOpen(_inputPin))
            {
                _controller.ClosePin(_inputPin);
            }

            _controller.Dispose();
        }
    }
}
