export interface Request {
    id: number,
    title: string,
    responseReason: string,
    status: number
}

export interface ApproveOrRejectRequest {
    responseReason: string
}
