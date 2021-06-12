/* tslint:disable */
/* eslint-disable */
/**
 * Playground
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: v1
 * 
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */

import { exists, mapValues } from '../runtime';
import {
    CustomersModel,
    CustomersModelFromJSON,
    CustomersModelFromJSONTyped,
    CustomersModelToJSON,
} from './';

/**
 * 
 * @export
 * @interface CustomersCreateResult
 */
export interface CustomersCreateResult {
    /**
     * 
     * @type {CustomersModel}
     * @memberof CustomersCreateResult
     */
    customer: CustomersModel;
}

export function CustomersCreateResultFromJSON(json: any): CustomersCreateResult {
    return CustomersCreateResultFromJSONTyped(json, false);
}

export function CustomersCreateResultFromJSONTyped(json: any, ignoreDiscriminator: boolean): CustomersCreateResult {
    if ((json === undefined) || (json === null)) {
        return json;
    }
    return {
        
        'customer': CustomersModelFromJSON(json['customer']),
    };
}

export function CustomersCreateResultToJSON(value?: CustomersCreateResult | null): any {
    if (value === undefined) {
        return undefined;
    }
    if (value === null) {
        return null;
    }
    return {
        
        'customer': CustomersModelToJSON(value.customer),
    };
}

