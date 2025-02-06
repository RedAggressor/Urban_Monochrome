import { ReactNode } from 'react';
import cl from './container.module.scss';

interface Props {
  children: ReactNode;
  className?: string;
  style?: React.CSSProperties;
}

export const Container: React.FC<Props> = ({ children, className, style }) => {
  return (
    <div className={`${cl.container} ${className}`} style={style}>
      {children}
    </div>
  );
};
