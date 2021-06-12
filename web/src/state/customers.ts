import { useMutation, useQuery, useQueryClient } from "react-query";
import { CustomerApi, CustomersCreateForm, CustomersUpdateForm } from "../api";
import { Config } from "../config";
import { GridQuery, useGridDataSource } from "./grid";

const api = new CustomerApi(Config.Api);

export function useCustomerGrid() {
  return useGridDataSource((query: GridQuery) => ({
    queryKey: ["customers", query],
    queryFn: async () =>
      await api.customerGet({
        ...query,
      }),
  }));
}

export function useCustomerQuery(id: number) {
  return useQuery({
    queryKey: ["customers", id],
    queryFn: async () => api.customerIdGet({ id }),
  });
}

export function useCustomerCreate() {
  const queryClient = useQueryClient();

  return useMutation(
    async (form: CustomersCreateForm) =>
      await api.customerPost({ customersCreateForm: form }),
    {
      onSuccess() {
        queryClient.invalidateQueries("customers");
      },
    }
  );
}

export function useCustomerUpdate(id: number) {
  const queryClient = useQueryClient();

  return useMutation(
    async (form: CustomersUpdateForm) =>
      await api.customerIdPost({ id, customersUpdateForm: form }),
    {
      onSuccess() {
        queryClient.invalidateQueries("customers");
      },
    }
  );
}
