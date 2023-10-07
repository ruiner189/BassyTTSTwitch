namespace BassyTTSTwitch.Audio
{
    public struct DeviceInfo
    {
        public int DeviceID;
        public string DeviceName;

        public DeviceInfo(int deviceID, string deviceName)
        {
            DeviceID = deviceID;
            DeviceName = deviceName;
        }

        public override string ToString()
        {
            return DeviceName;
        }
    }
}
