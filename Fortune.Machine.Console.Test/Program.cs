using Fortune.Machine.Services.Internal;
using System.Device.Gpio;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Raspberry Pi GPIO LED Flash Test with Input Handling");

        try
        {
            using (GpioService gpioService = new GpioService())
            {
                using (InputHandlerService inputHandlerService = new InputHandlerService(gpioService))
                {
                    inputHandlerService.StartListening();
                }
            }
        } catch (PlatformNotSupportedException ex)
        {
            Console.WriteLine("GPIO is not supported on this platform.");
            Console.WriteLine($"Exception: {ex.Message}");
        } catch (Exception ex)
        {
            Console.WriteLine("An error occurred while accessing the GPIO.");
            Console.WriteLine($"Exception: {ex.Message}");
        }
    }
}