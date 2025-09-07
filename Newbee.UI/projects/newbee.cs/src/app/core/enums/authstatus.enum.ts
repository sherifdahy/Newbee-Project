export enum AuthStatus {
  Valid,          // التوكن لسه شغال
  AccessExpired,  // محتاج تجديد Access Token
  RefreshExpired  // لازم Login جديد
}
