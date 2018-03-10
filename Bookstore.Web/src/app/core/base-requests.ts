import {
  Headers,
  Http,
  RequestOptions,
  RequestOptionsArgs,
  Response,
  ResponseContentType
} from '@angular/http';
import { Observable } from 'rxjs/Observable';
declare var swal: any;

export class BaseRequests {
  getHeaders(): Headers {
    const headers = new Headers();
    headers.append('Access-Control-Allow-Origin', '*');
    return headers;
  }

  getOptionsHeader(): RequestOptions {
    return new RequestOptions({ headers: this.getHeaders() });
  }

  handleError(error): any {
    const errorObj = error.json();
    console.error('Ocorreu um erro', errorObj);

    swal({
      type: 'error',
      title: 'Ocorreu um erro',
      text: errorObj.Message,
    });

    return Observable.throw(error);
  }
}
