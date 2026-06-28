import { cn } from "@/lib/utils";

type IconProps = {
  name: string;
  className?: string;
  filled?: boolean;
};

export function Icon({ name, className, filled }: IconProps) {
  return (
    <span
      className={cn(
        "material-symbols-outlined leading-none",
        filled && "fill",
        className
      )}
    >
      {name}
    </span>
  );
}
