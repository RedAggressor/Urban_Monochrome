import { ReactNode } from 'react';
import cl from './container.module.scss';

interface Props {
  children: ReactNode;
  className?: string;
  ref?: React.Ref<HTMLDivElement>;
  style?: React.CSSProperties;
}

export const Container: React.FC<Props> = ({
  children,
  className,
  ref,
  style,
}) => {
  return (
    <div className={`${cl.container} ${className}`} style={style} ref={ref}>
      {children}
    </div>
  );
};
