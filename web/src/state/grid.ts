import { GridPageChangeParams } from "@material-ui/data-grid";
import { useState } from "react";
import { useQuery, FetchQueryOptions } from "react-query";

export interface Paginated<T> {
  items: T[];
  page: number;
  pageSize: number;
  count: number;
  total: number;
}

export interface GridQuery {
  page: number;
  pageSize: number;
}

export interface GridDataResponse<T> {
  data: Paginated<T>;
  isFetching: boolean;
  error: unknown;
}

export function useGridDataSource<T>(
  dataSource: (query: GridQuery) => FetchQueryOptions<Paginated<T>>
) {
  const [page, setPage] = useState(0);
  const [pageSize, setPageSize] = useState(10);

  // grid uses 0-index page
  const pageNo = page + 1;

  const { data, isFetching, error } = useQuery({
    ...{ keepPreviousData: true, staleTime: 5000 },
    ...dataSource({ page: pageNo, pageSize }),
  });

  return {
    loading: isFetching,
    error,
    rows: data?.items ?? [],
    page,
    pageSize,
    rowCount: data?.total,
    paginationMode: "server" as const,
    onPageChange({ page, pageSize }: GridPageChangeParams) {
      setPage(page);
      setPageSize(pageSize);
    },
    setPageSize({ page, pageSize }: GridPageChangeParams) {
      setPage(page);
      setPageSize(pageSize);
    },
  };
}
