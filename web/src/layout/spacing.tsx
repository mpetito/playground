import { Box, styled, Theme } from "@material-ui/core";

export const Spacing = styled(Box)<Theme, { gap?: number }>(
  ({ theme, gap = 1 }) => ({
    display: "flex",
    flexWrap: "wrap",
    gap: theme.spacing(gap),
  })
);
