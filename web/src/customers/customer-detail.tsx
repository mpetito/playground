import React from "react";

import { Form } from "react-final-form";
import { makeValidate, TextField } from "mui-rff";
import * as yup from "yup";

import { Paper } from "@material-ui/core";
import Layout from "../layout";
import { CancelButton, DeleteButton, SaveButton } from "../components/buttons";
import { useCustomerQuery, useCustomerUpdate } from "../state/customers";

interface Props {
  customerId: number;
}

const schema = yup.object({
  name: yup.string().label("Name").required(),
  phone: yup.string().label("Phone").required(),
  address: yup.object({
    state: yup.string().label("State").required(),
  }),
});

const validate = makeValidate(schema);

export const CustomerDetail: React.FC<Props> = ({ customerId }) => {
  const { data, isLoading } = useCustomerQuery(customerId);
  const { mutate } = useCustomerUpdate(customerId);

  if (!data) return null;

  return (
    <Paper>
      <Form
        initialValues={data.customer}
        validate={validate}
        onSubmit={(form) => {
          console.log(form);
          mutate(form);
        }}
        render={({ handleSubmit }) => (
          <form onSubmit={handleSubmit}>
            <Layout.Stack>
              <TextField name="name" label="Full Name" required />
              <Layout.Stack>
                <TextField name="phone" label="Phone" type="tel" required />
                <TextField name="address.state" label="State" required />
              </Layout.Stack>
              <Layout.Justify
                left={[<SaveButton />, <CancelButton />]}
                right={<DeleteButton />}
              />
            </Layout.Stack>
          </form>
        )}
      />
    </Paper>
  );
};
