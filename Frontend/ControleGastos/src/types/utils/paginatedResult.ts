export interface PaginatedResult<T> {
  data: T[];
  pageIndex: number;
  pageSize: number;
  totalCount: number;
  count: number;
}
