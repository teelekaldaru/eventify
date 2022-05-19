import { Injectable } from "@angular/core";
import { PaymentMethod } from "../../models/finances/payment-method.model";
import { CommonEndpointService } from "../common-endpoint.service";

@Injectable()
export class FinanceService {
    constructor(private readonly endpoint: CommonEndpointService) {}

    private readonly relativeUrl = '/api/finances';

    getPaymentMethods() {
        return this.endpoint.get<PaymentMethod[]>(`${this.relativeUrl}/payment-methods`);
    }
}
