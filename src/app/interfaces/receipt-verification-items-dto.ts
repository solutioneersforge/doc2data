export interface ReceiptVerificationItemsDTO {
    receiptItemID: string; 
    itemDescription: string;
    quantity: number;
    unitPrice: number;
    discount: number;
    total: number;
    subCategoryId: number;
    subCategoryName: string;
}
