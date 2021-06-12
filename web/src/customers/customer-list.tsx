import React from "react";

import { DataGrid, GridColDef } from "@material-ui/data-grid";

import { useCustomerGrid } from "../state/customers";

const columns: GridColDef[] = [
  { field: "id", headerName: "ID", flex: 1 },
  { field: "name", headerName: "Name", flex: 2 },
  { field: "phone", headerName: "Email", flex: 2 },
  {
    field: "address.state",
    headerName: "State",
    flex: 1,
  },
];

interface Props {
  onSelected(id: number): void;
}

export const CustomerList: React.FC<Props> = ({ onSelected }) => {
  const grid = useCustomerGrid();

  return (
    <DataGrid
      {...grid}
      columns={columns}
      autoHeight
      onRowSelected={({ data }) => onSelected(data.id)}
    />
  );
};
