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
            _controller.OpenPin(_inputPin, PinMode.InputPullUp);
        }

        public void StartListening()
        {
            Console.WriteLine("Listening for button press...");

            while (true)
            {
                if (_controller.Read(_inputPin) == PinValue.Low)
                {
                    Console.WriteLine("Button pressed!");

                    _gpioService.FlashLed(Settings.LedFlashCount, Settings.LedFlashIntervalMilliseconds);
                }

                Thread.Sleep(100);
            }
        }

        public void Dispose()
        {
            if (_controller.IsPinOpen(_inputPin))
            {
                _controller.ClosePin(_inputPin);
            }

            _controller.Dispose();
        }
    }
}
