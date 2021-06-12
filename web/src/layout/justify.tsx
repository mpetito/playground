import React, { ReactElement } from "react";
import { Grid, GridProps } from "@material-ui/core";
import { Spacing } from "./spacing";

type Children = ReactElement | ReactElement[];

export interface JustifyProps {
  children?: Children;
  left?: Children;
  right?: Children;
}

export const Justify = ({ left, children: middle, right }: JustifyProps) => {
  return (
    <Grid container spacing={1}>
      <Side>{assignKeys(left)}</Side>
      <Middle>{assignKeys(middle)}</Middle>
      <Side>{assignKeys(right)}</Side>
    </Grid>
  );
};

interface SectionProps {
  children?: Children;
}

const Side = ({ children }: SectionProps) => {
  if (!children) return null;

  return (
    <Grid item>
      <Spacing>{children}</Spacing>
    </Grid>
  );
};

const Middle = ({ children }: SectionProps) => {
  if (!children) return <Grid item xs />;

  return (
    <Grid item xs>
      <Spacing>{children}</Spacing>
    </Grid>
  );
};

function assignKeys(children?: Children) {
  if (!children) return undefined;

  return React.Children.map(children, (child, key) =>
    React.cloneElement(child, { key })
  );
}
