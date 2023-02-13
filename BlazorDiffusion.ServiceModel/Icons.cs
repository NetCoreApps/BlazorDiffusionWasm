// Complete declarative AutoQuery services for Bookings CRUD example:
// https://docs.servicestack.net/autoquery-crud-bookings

namespace BlazorDiffusion.ServiceModel;

public static class Icons
{
    public const string Creative = "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 15 15'><path fill='currentColor' d='M10.71 3L7.85.15a.5.5 0 0 0-.707-.003L7.14.15L4.29 3H1.5a.5.5 0 0 0-.5.5v9a.5.5 0 0 0 .5.5h12a.5.5 0 0 0 .5-.5v-9a.5.5 0 0 0-.5-.5zM7.5 1.21L9.29 3H5.71zM13 12H2V4h11zM5 7a1 1 0 1 1 0-2a1 1 0 0 1 0 2zm7 4H4.5L6 8l1.25 2.5L9.5 6z'/></svg>";
    public const string Artifact = "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 16 16'><g fill='currentColor'><path d='M8.002 5.5a1.5 1.5 0 1 1-3 0a1.5 1.5 0 0 1 3 0z'/><path d='M12 0H4a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h8a2 2 0 0 0 2-2V2a2 2 0 0 0-2-2zM3 2a1 1 0 0 1 1-1h8a1 1 0 0 1 1 1v8l-2.083-2.083a.5.5 0 0 0-.76.063L8 11L5.835 9.7a.5.5 0 0 0-.611.076L3 12V2z'/></g></svg>";
    public const string Artist = "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 32 32'><g fill='currentColor'><path d='M24 19a3 3 0 1 0 0-6a3 3 0 0 0 0 6Zm0-1a2 2 0 1 1 0-4a2 2 0 0 1 0 4Zm-7.5-9.25a2.25 2.25 0 1 1-4.5 0a2.25 2.25 0 0 1 4.5 0Zm-6 4a2.25 2.25 0 1 1-4.5 0a2.25 2.25 0 0 1 4.5 0ZM8.25 22a2.25 2.25 0 1 0 0-4.5a2.25 2.25 0 0 0 0 4.5ZM16 24.25a2.25 2.25 0 1 1-4.5 0a2.25 2.25 0 0 1 4.5 0Z'/><path d='M16.2 31a16.717 16.717 0 0 1-7.84-2.622a15.045 15.045 0 0 1-6.948-9.165A13.032 13.032 0 0 1 2.859 9.22c3.757-6.2 12.179-8.033 19.588-4.256c4.419 2.255 7.724 6.191 8.418 10.03a6.8 6.8 0 0 1-1.612 6.02c-2.158 2.356-4.943 2.323-6.967 2.3h-.007c-1.345-.024-2.185 0-2.386.4c.07.308.192.604.36.873a3.916 3.916 0 0 1-.209 4.807A4.7 4.7 0 0 1 16.2 31ZM14.529 5a11.35 11.35 0 0 0-9.961 5.25a11.048 11.048 0 0 0-1.218 8.473a13.03 13.03 0 0 0 6.03 7.934c3.351 1.988 7.634 3.3 9.111 1.473c.787-.968.537-1.565-.012-2.622a2.843 2.843 0 0 1-.372-2.7c.781-1.54 2.518-1.523 4.2-1.5c1.835.025 3.917.05 5.472-1.649a4.909 4.909 0 0 0 1.12-4.314c-.578-3.2-3.536-6.653-7.358-8.6a15.482 15.482 0 0 0-7.01-1.74L14.529 5Z'/></g></svg>";
    public const string Modifier = "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path fill='currentColor' d='M19.75 2A2.25 2.25 0 0 1 22 4.25v5.462a3.25 3.25 0 0 1-.952 2.298l-8.5 8.503a3.255 3.255 0 0 1-4.597.001L3.489 16.06a3.25 3.25 0 0 1-.004-4.596l8.5-8.51a3.25 3.25 0 0 1 2.3-.953h5.465Zm0 1.5h-5.466c-.464 0-.91.185-1.238.513l-8.512 8.523a1.75 1.75 0 0 0 .015 2.462l4.461 4.454a1.755 1.755 0 0 0 2.477 0l8.5-8.503a1.75 1.75 0 0 0 .513-1.237V4.25a.75.75 0 0 0-.75-.75ZM17 5.502a1.5 1.5 0 1 1 0 3a1.5 1.5 0 0 1 0-3Z'/></svg>";
    public const string Report = "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path fill='currentColor' d='M12 5.99L19.53 19H4.47L12 5.99M12 2L1 21h22L12 2zm1 14h-2v2h2v-2zm0-6h-2v4h2v-4z'/></svg>";
    public const string Album = "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path fill='currentColor' d='M11.024 11.536L10 10l-2 3h9l-3.5-5z'/><circle cx='9.503' cy='7.497' r='1.503' fill='currentColor'/><path fill='currentColor' d='M19 2H6c-1.206 0-3 .799-3 3v14c0 2.201 1.794 3 3 3h15v-2H6.012C5.55 19.988 5 19.806 5 19s.55-.988 1.012-1H21V4c0-1.103-.897-2-2-2zm0 14H5V5c0-.806.55-.988 1-1h13v12z'/></svg>";
    public const string Stats = "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path fill='currentColor' d='M8.143 15.857H5.57V9.43h2.572v6.428zm5.143 0h-2.572V3h2.572v12.857zm5.142 0h-2.571v-9h2.571v9z'/><path fill='currentColor' fill-rule='evenodd' d='M21 20.714H3v-2h18v2z' clip-rule='evenodd'/></svg>";
    //public const string AnonUser = "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path fill='#556080' d='M7.5 6.5C7.5 8.981 9.519 11 12 11s4.5-2.019 4.5-4.5S14.481 2 12 2S7.5 4.019 7.5 6.5zM20 21h1v-1c0-3.859-3.141-7-7-7h-4c-3.86 0-7 3.141-7 7v1h17z'/></svg>";
    public const string AnonUser = "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path fill='#556080' d='M12 2a5 5 0 1 0 5 5a5 5 0 0 0-5-5zm0 8a3 3 0 1 1 3-3a3 3 0 0 1-3 3zm9 11v-1a7 7 0 0 0-7-7h-4a7 7 0 0 0-7 7v1h2v-1a5 5 0 0 1 5-5h4a5 5 0 0 1 5 5v1z'/></svg>";
    public const string AnonUserUri = "data:image/svg+xml,%3Csvg style='color:rgb(8 145 178);border-radius: 9999px;overflow: hidden;' xmlns='http://www.w3.org/2000/svg' viewBox='0 0 200 200'%3E%3Cpath fill='currentColor' d='M200,100 a100,100 0 1 0 -167.3,73.9 a3.6,3.6 0 0 0 0.9,0.8 a99.9,99.9 0 0 0 132.9,0 l0.8,-0.8 A99.6,99.6 0 0 0 200,100 zm-192,0 a92,92 0 1 1 157.2,64.9 a75.8,75.8 0 0 0 -44.5,-34.1 a44,44 0 1 0 -41.4,0 a75.8,75.8 0 0 0 -44.5,34.1 A92.1,92.1 0 0 1 8,100 zm92,28 a36,36 0 1 1 36,-36 a36,36 0 0 1 -36,36 zm-59.1,42.4 a68,68 0 0 1 118.2,0 a91.7,91.7 0 0 1 -118.2,0 z' /%3E%3C/svg%3E%0A";
    public const string Signup = "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 16 16'><g fill='currentColor'><path fill-rule='evenodd' d='M10.854 7.146a.5.5 0 0 1 0 .708l-3 3a.5.5 0 0 1-.708 0l-1.5-1.5a.5.5 0 1 1 .708-.708L7.5 9.793l2.646-2.647a.5.5 0 0 1 .708 0z'/><path d='M4 1.5H3a2 2 0 0 0-2 2V14a2 2 0 0 0 2 2h10a2 2 0 0 0 2-2V3.5a2 2 0 0 0-2-2h-1v1h1a1 1 0 0 1 1 1V14a1 1 0 0 1-1 1H3a1 1 0 0 1-1-1V3.5a1 1 0 0 1 1-1h1v-1z'/><path d='M9.5 1a.5.5 0 0 1 .5.5v1a.5.5 0 0 1-.5.5h-3a.5.5 0 0 1-.5-.5v-1a.5.5 0 0 1 .5-.5h3zm-3-1A1.5 1.5 0 0 0 5 1.5v1A1.5 1.5 0 0 0 6.5 4h3A1.5 1.5 0 0 0 11 2.5v-1A1.5 1.5 0 0 0 9.5 0h-3z'/></g></svg>";
}