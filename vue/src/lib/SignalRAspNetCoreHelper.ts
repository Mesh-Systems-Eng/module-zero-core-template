// Uncomment to use signalr

// import AppConsts from './appconst'
// import Util from './util'
// class SignalRAspNetCoreHelper{
//     initSignalR(){
//         var encryptedAuthToken = Util.abp.utils.getCookieValue(AppConsts.authorization.encrptedAuthTokenName);

//         Util.abp.signalr = {
//             autoConnect: true,
//             connect: undefined,
//             hubs: undefined,
//             qs: AppConsts.authorization.encrptedAuthTokenName + "=" + encodeURIComponent(encryptedAuthToken),
//             remoteServiceBaseUrl: AppConsts.remoteServiceBaseUrl,
//             url: '/signalr'
//         };

//         Util.loadScript(AppConsts.appBaseUrl + '/dist/abp.signalr-client.js');
//     }
// }
// export default new SignalRAspNetCoreHelper();