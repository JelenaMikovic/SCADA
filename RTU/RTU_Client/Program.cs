using RTU;
using System.Text;
using System.Text.Json;


public class Program
{

    private const string api = "https://localhost:7012/api/Device";
    private static List<Device> devices = new List<Device>();
    private static Random rand = new Random();

    public static Task Main()
    {
        initilizeDevices();
        readValues();
        return Task.CompletedTask;
    }

    private static void initilizeDevices()
    {
        for(int i  = 0; i < 5; i++) {
            devices.Add(new Device { LowLimit = rand.Next(-100, 10), HighLimit = rand.Next(11, 100), Type = DeviceType.DI, Value = 0, IOAddress = "DI.168.172.21." + i});
        }

        for (int i = 5; i < 10; i++)
        {
            devices.Add(new Device { LowLimit = rand.Next(-100, 10), HighLimit = rand.Next(11, 100), Type = DeviceType.AI, Value = 0, IOAddress = "AI.168.172.21." + i });
        }
        List<DeviceDTO> deviceDTOs = new List<DeviceDTO>();
        foreach (Device device in devices)
        {
            if (device.Type.Equals(DeviceType.DI))
            {
                device.Value = rand.Next() % 2;
            }
            else { device.Value = rand.NextDouble() * (device.HighLimit - device.LowLimit) + device.LowLimit; }
            deviceDTOs.Add(new DeviceDTO { IOAddress = device.IOAddress, Type = device.Type.ToString(), Value = device.Value });
        }
        sendRequestCreate(deviceDTOs);
        Thread.Sleep(3000);
    }

    private static void readValues()
    {
        while (true)
        {
            List<DeviceDTO> deviceDTOs = new List<DeviceDTO>();
            foreach (Device device in devices)
            {
                if (device.Type.Equals(DeviceType.DI))
                {
                    device.Value = rand.Next() % 2;
                } else { device.Value = rand.NextDouble() * (device.HighLimit - device.LowLimit) + device.LowLimit; }
                deviceDTOs.Add(new DeviceDTO { IOAddress = device.IOAddress, Type = device.Type.ToString(), Value = device.Value });
            }
            //sendRequest(deviceDTOs);
            Thread.Sleep(1000);
        }
    }

    private static async void sendRequest(List<DeviceDTO> deviceDTOs)
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                var serializedEntries = JsonSerializer.Serialize(deviceDTOs);
                var requestContent = new StringContent(serializedEntries, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync(api, requestContent);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseBody);
                }
                else
                {
                    Console.WriteLine($"HTTP request failed with status code: {response.StatusCode}");
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseBody);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }

    private static async void sendRequestCreate(List<DeviceDTO> deviceDTOs)
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                var serializedEntries = JsonSerializer.Serialize(deviceDTOs);
                var requestContent = new StringContent(serializedEntries, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(api, requestContent);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseBody);
                }
                else
                {
                    Console.WriteLine($"HTTP request failed with status code: {response.StatusCode}");
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseBody);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}