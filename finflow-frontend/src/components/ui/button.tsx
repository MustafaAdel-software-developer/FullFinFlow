import * as React from "react";
import { Slot } from "@radix-ui/react-slot";
import { cva, type VariantProps } from "class-variance-authority";
import { cn } from "@/lib/utils";

const buttonVariants = cva(
  "inline-flex items-center justify-center gap-2 whitespace-nowrap rounded-lg font-semibold transition-all focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-primary/50 disabled:pointer-events-none disabled:opacity-50 [&_.material-symbols-outlined]:text-[1.125rem]",
  {
    variants: {
      variant: {
        default:
          "bg-primary text-primary-foreground hover:brightness-110",
        outline:
          "border border-border-dark bg-transparent text-foreground hover:bg-card-dark",
        ghost:
          "text-muted-foreground hover:bg-card-dark hover:text-foreground",
        secondary:
          "bg-card-dark border border-border-dark text-foreground hover:bg-muted",
        destructive: "bg-destructive text-white hover:bg-destructive/90",
      },
      size: {
        default: "h-10 px-4 py-2 text-sm",
        sm: "h-9 px-4 py-2 text-sm",
        lg: "h-11 px-6 text-base",
        icon: "size-9 shrink-0 p-0",
      },
    },
    defaultVariants: { variant: "default", size: "default" },
  }
);

export interface ButtonProps
  extends React.ButtonHTMLAttributes<HTMLButtonElement>,
    VariantProps<typeof buttonVariants> {
  asChild?: boolean;
}

const Button = React.forwardRef<HTMLButtonElement, ButtonProps>(
  ({ className, variant, size, asChild = false, ...props }, ref) => {
    const Comp = asChild ? Slot : "button";
    return (
      <Comp className={cn(buttonVariants({ variant, size, className }))} ref={ref} {...props} />
    );
  }
);
Button.displayName = "Button";

export { Button, buttonVariants };
