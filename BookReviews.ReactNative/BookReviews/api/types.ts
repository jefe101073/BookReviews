export interface Author {
    id: number,
    firstName: string,
    lastName: string,
    isDeleted: boolean,
    deletedOn: Date,
    deletedByUserId: number
}