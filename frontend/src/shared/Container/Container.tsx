import { ReactNode } from 'react';
import cl from './container.module.scss';

interface Props {
  children: ReactNode;
  className?: string;
}

export const Container: React.FC<Props> = ({ children, className }) => {
  return <div className={`${cl.container} ${className}`}>{children}</div>;
};
