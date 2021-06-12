import React, { useState } from "react";
import { QueryClient, QueryClientProvider } from "react-query";
import { ReactQueryDevtools } from "react-query/devtools";

import { ThemeProvider } from "@material-ui/core";
import { theme } from "./theme";

import { CustomerList } from "./customers/customer-list";
import { CustomerDetail } from "./customers/customer-detail";

export const queryClient = new QueryClient();

function App() {
  const [customerId, setCustomerId] = useState<number>();
  return (
    <QueryClientProvider client={queryClient}>
      <ThemeProvider theme={theme}>
        <CustomerList onSelected={setCustomerId} />
        {customerId && <CustomerDetail customerId={customerId} />}
      </ThemeProvider>
      <ReactQueryDevtools initialIsOpen={false} />
    </QueryClientProvider>
  );
}

export default App;
