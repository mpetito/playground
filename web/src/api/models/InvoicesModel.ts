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
/**
 * 
 * @export
 * @interface InvoicesModel
 */
export interface InvoicesModel {
    /**
     * 
     * @type {number}
     * @memberof InvoicesModel
     */
    id: number;
    /**
     * 
     * @type {Date}
     * @memberof InvoicesModel
     */
    createdAt: Date;
}

export function InvoicesModelFromJSON(json: any): InvoicesModel {
    return InvoicesModelFromJSONTyped(json, false);
}

export function InvoicesModelFromJSONTyped(json: any, ignoreDiscriminator: boolean): InvoicesModel {
    if ((json === undefined) || (json === null)) {
        return json;
    }
    return {
        
        'id': json['id'],
        'createdAt': (new Date(json['createdAt'])),
    };
}

export function InvoicesModelToJSON(value?: InvoicesModel | null): any {
    if (value === undefined) {
        return undefined;
    }
    if (value === null) {
        return null;
    }
    return {
        
        'id': value.id,
        'createdAt': (value.createdAt.toISOString()),
    };
}

