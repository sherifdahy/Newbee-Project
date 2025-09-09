export enum AuthStatus {
  emptyTokens,
  valid, // التوكن لسه شغال
  tokenExpired, // محتاج تجديد Access Token
  refreshTokenExpired, // لازم Login جديد
}
