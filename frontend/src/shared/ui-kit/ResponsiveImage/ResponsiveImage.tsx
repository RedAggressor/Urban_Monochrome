type Source = {
  media: string;
  srcSet: string;
};

type Img = {
  src: string;
  alt: string;
};

type Props = {
  sources: Source[];
  img: Img;
  className?: string;
};

export const ResponsiveImage: React.FC<Props> = ({ sources, img, className }) => {
  return (
    <picture>
      {sources.map((source, index) => (
        <source
          key={index}
          media={source.media}
          srcSet={source.srcSet}
        />
      ))}
      <img
        src={img.src}
        alt={img.alt}
        className={className}
        loading="lazy"
      />
    </picture>
  );
};
