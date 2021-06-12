import React from "react";
import { Button, ButtonProps, Toolbar } from "@material-ui/core";

export const SaveButton: React.FC<ButtonProps> = (props) => (
  <Button type="submit" variant="contained" color="primary" {...props}>
    Save
  </Button>
);

export const CancelButton: React.FC<ButtonProps> = (props) => (
  <Button type="button" variant="outlined" {...props}>
    Cancel
  </Button>
);

export const DeleteButton: React.FC<ButtonProps> = (props) => (
  <Button type="button" variant="contained" {...props}>
    Delete
  </Button>
);
