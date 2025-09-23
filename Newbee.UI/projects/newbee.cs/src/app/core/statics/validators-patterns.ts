export class ValidatorPatterns {
  static readonly StrongPassword =
    /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()\-+=\[\]{};:,<.>]).{8,}$/;
}
