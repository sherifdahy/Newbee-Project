export interface IApiErrorVm {
  type: string;
  title: string;
  status: number;
  errors: { [key: string]: string[] }; // بدل string[]
}
