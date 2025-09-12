import { ILoginResponse } from '../view-models/responses/login-response-vm';
import { IRefreshTokenStoreVm } from '../view-models/stores/refresh-token-store-vm';
import { ITokenStoreVm } from '../view-models/stores/token-store-vm';
import { IUserVm } from '../view-models/stores/user-vm';

export class AuthMapper {
  static mapToTokenStore(res: ILoginResponse): ITokenStoreVm {
    return {
      token: res.token,
      expiresInSeconds: res.expiresIn,
      createdAt: new Date().toISOString(),
    };
  }

  static mapToRefreshTokenStore(res: ILoginResponse): IRefreshTokenStoreVm {
    return {
      refreshToken: res.refreshToken,
      expiresAt: res.refreshTokenExpiration,
      createdAt: new Date().toISOString(),
    };
  }

  static mapToUserStore(res: ILoginResponse): IUserVm {
    return {
      userId: res.userId,
      firstName: res.firstName,
      lastName: res.lastName,
      email: res.email,
    };
  }
}
