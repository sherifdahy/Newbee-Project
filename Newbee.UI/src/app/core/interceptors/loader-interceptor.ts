import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { delay, finalize, Observable } from "rxjs";
import { LoaderService } from "../services/loader.service";

@Injectable()
export class loaderInterceptor implements HttpInterceptor {
  constructor(private loaderService: LoaderService) {

  }
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    this.loaderService.loading();
    return next.handle(req).pipe(
      finalize(() => this.loaderService.hide())
    );
  }
};
