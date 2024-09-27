using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortune.Machine.Services.Internal
{
    public class GpioService : IDisposable
    {
        private readonly GpioController _controller;
        private readonly int _ledPin;

        public GpioService()
        {
            _ledPin = Settings.LedPin;
            _controller = new GpioController(PinNumberingScheme.Board);

            // Open the LED pin for output
            _controller.OpenPin(_ledPin, PinMode.Output);
        }

        public void TurnLedOn()
        {
            _controller.Write(_ledPin, PinValue.High);
            Console.WriteLine("LED ON");
        }

        public void TurnLedOff()
        {
            _controller.Write(_ledPin, PinValue.Low);
            Console.WriteLine("LED OFF");
        }

        public void FlashLed(int times, int intervalMilliseconds)
        {
            for (int i = 0; i < times; i++)
            {
                TurnLedOn();
                Thread.Sleep(intervalMilliseconds);
                TurnLedOff();
                Thread.Sleep(intervalMilliseconds);
            }
        }

        public void Dispose()
        {
            // Clean up and close the pin
            if (_controller.IsPinOpen(_ledPin))
            {
                _controller.ClosePin(_ledPin);
            }

            _controller.Dispose();
        }
    }
}
