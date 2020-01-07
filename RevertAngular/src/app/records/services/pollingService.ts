import { Observable, ReplaySubject } from '../../../../node_modules/rxjs';

export class PollingService {
    private observable: Observable<any>;
    private destroyed = false;

    constructor() { }

    destroy() {
        this.destroyed = true;
    }

    create<T>(
        interval: number,
        input: T,
        verifyMethod: (t: T) => Observable<boolean>,
        getMethod: () => Observable<T[]>
    ): Observable<T[]> {
        this.observable = new Observable((observer) => {
            setInterval(() => {
                if (this.destroyed) {
                    observer.complete();
                    return;
                }
                verifyMethod(input).subscribe((result) => {
                    if (result) {
                        getMethod().subscribe(results => {
                            input = results[0];
                            observer.next(results);
                        });
                    }
                });
            }, interval * 1000);
        });

        return this.observable;
    }
}
