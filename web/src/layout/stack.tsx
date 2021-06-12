import React from "react";
import { Box, Grid, GridSize, GridSpacing } from "@material-ui/core";
import { Breakpoint } from "@material-ui/core/styles/createBreakpoints";

export interface StackProps {
  spacing?: GridSpacing;
  nested?: boolean;
  horizontal?: boolean;
  breakpoint?: Breakpoint;
}

const FlexWidth = true;
const FullWidth: GridSize = 12;
const DefaultSpacing: GridSpacing = 2;
const DefaultBreakpoint: Breakpoint = "md";

export const Stack: React.FC<StackProps> = ({ children, ...props }) => {
  const { spacing = DefaultSpacing, nested } = props;
  const itemProps = getItemProps(props);

  return (
    <Box padding={nested ? 0 : spacing}>
      <Grid container spacing={spacing}>
        {React.Children.map(children, (child, i) => (
          <Grid item key={i} {...itemProps}>
            {nestChild(child, props)}
          </Grid>
        ))}
      </Grid>
    </Box>
  );
};

function getItemProps({
  horizontal,
  breakpoint = DefaultBreakpoint,
}: StackProps) {
  if (horizontal) {
    return {
      // one item per row at smaller resolutions
      xs: FullWidth,
      // equally spaced items at medium resolutions and up
      [breakpoint]: FlexWidth,
    };
  }

  return {
    // one item per row
    xs: FullWidth,
  };
}

function nestChild(child: any, { horizontal }: StackProps) {
  if (child && child.type === Stack) {
    return React.cloneElement(child, {
      nested: true,
      horizontal: !horizontal,
    });
  }

  return child;
}
